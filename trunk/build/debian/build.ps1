$tgt
$port
$key

rmif ./package/opt
mkdir ./package/opt/pivote
cp -r ../../client/bin/release/* ./package/opt/pivote/
rmif ./package/opt/pivote/*vshost*
$svndirs = ls -r -force -filter .svn ./package/opt
foreach ($svndir in $svndirs)
{
	rmif $svndir.FullName
}

rmif pivote-client.deb
plink -i $key -pw $pass -P $port $tgt rm -r package
plink -i $key -pw $pass -P $port $tgt rm pivote-client.deb
pscp -i $key -pw $pass -P $port -r package $tgt
plink -i $key -pw $pass -P $port $tgt chmod u+x package/usr/bin/pivote
plink -i $key -pw $pass -P $port $tgt dpkg -b package pivote-client.deb
pscp -i $key -pw $pass -P $port $tgt/pivote-client.deb .

rmif ./package/opt