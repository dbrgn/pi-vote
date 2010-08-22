$tgt
$port
$key

rmif ./package/opt
mkdir ./package/opt/pivote
cp -r ../../client/bin/release/* ./package/opt/pivote/
rmif ./package/opt/pivote/*vshost*
cp -r ../files-linux/* ./package/opt/pivote/

cp -r package packagetmp
$svndirs = ls -r -force -filter .svn ./packagetmp
foreach ($svndir in $svndirs)
{
	rmif $svndir.FullName
}

rmif pivote-client.deb
plink -i $key -pw $pass -P $port $tgt rm -r packagetmp
plink -i $key -pw $pass -P $port $tgt rm pivote-client.deb
pscp -i $key -pw $pass -P $port -r packagetmp $tgt
plink -i $key -pw $pass -P $port $tgt chmod u+x packagetmp/usr/bin/pivote
plink -i $key -pw $pass -P $port $tgt chmod 0755 packagetmp/DEBIAN/postinst
plink -i $key -pw $pass -P $port $tgt chmod 0755 packagetmp/DEBIAN/postrm
plink -i $key -pw $pass -P $port $tgt dpkg -b packagetmp pivote-client.deb
pscp -i $key -pw $pass -P $port $tgt/pivote-client.deb .
plink -i $key -pw $pass -P $port $tgt rm -r packagetmp
plink -i $key -pw $pass -P $port $tgt rm pivote-client.deb

rmif ./packagetmp
rmif ./package/opt