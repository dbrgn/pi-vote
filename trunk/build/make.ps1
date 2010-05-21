param([string]$pass = $(throw "pass required."))

$tgt = "exception@lechuck.piratenpartei.ch:/home/exception/"
$port = 59922
$key = "D:\Security\PPS\piratenpartei.ch-ssh-rsa-4096.ppk"

rm pivote-client.deb
cd debian
./build.ps1
cd ..
cp debian/pivote-client.deb .