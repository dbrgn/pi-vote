$tgt
$port
$key

rm -r ./package/opt
mkdir ./package/opt/pivote
cp -r ../../client/bin/release/* ./package/opt/pivote/
rm ./package/opt/pivote/*vshost*

rm pivote-client.deb
plink -i $key -pw $pass -P $port $tgt rm -r package
plink -i $key -pw $pass -P $port $tgt rm pivote-client.deb
pscp -i $key -pw $pass -P $port -r package $tgt
plink -i $key -pw $pass -P $port $tgt chmod u+x package/usr/bin/pivote
plink -i $key -pw $pass -P $port $tgt dpkg -b package pivote-client.deb
pscp -i $key -pw $pass -P $port $tgt/pivote-client.deb .