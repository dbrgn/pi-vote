$tgt
$port
$key

rmif ./package/opt
mkdir ./package/opt/pivote
cp -r ../../client/bin/release/de-DE ./package/opt/pivote/
cp -r ../../client/bin/release/fr-FR ./package/opt/pivote/
cp ../../client/bin/release/Emil.GMP.dll ./package/opt/pivote/
cp ../../client/bin/release/MySql.Data.dll ./package/opt/pivote/
cp ../../client/bin/release/Pirate.PiVote.Client.exe ./package/opt/pivote/
cp ../../client/bin/release/Pirate.PiVote.dll ./package/opt/pivote/
cp -r ../files-linux/* ./package/opt/pivote/

cp -r package packagetmp
$svndirs = ls -r -force -filter .svn ./packagetmp
foreach ($svndir in $svndirs)
{
	rmif $svndir.FullName
}

rmif pivote-client.deb
plink -i $key -pw $pass -P $port $svr rm -r packagetmp
plink -i $key -pw $pass -P $port $svr rm pivote-client.deb
pscp -i $key -pw $pass -P $port -r packagetmp $tgt
plink -i $key -pw $pass -P $port $svr chmod u+x packagetmp/usr/bin/pivote
plink -i $key -pw $pass -P $port $svr chmod 0755 packagetmp/DEBIAN/postinst
plink -i $key -pw $pass -P $port $svr chmod 0755 packagetmp/DEBIAN/postrm
plink -i $key -pw $pass -P $port $svr dpkg -b packagetmp pivote-client.deb
pscp -i $key -pw $pass -P $port $tgt/pivote-client.deb .
plink -i $key -pw $pass -P $port $svr rm -r packagetmp
plink -i $key -pw $pass -P $port $svr rm pivote-client.deb

rmif ./packagetmp
rmif ./package/opt