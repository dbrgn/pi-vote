$tgt = "exception@lechuck.piratenpartei.ch:/home/exception/"
$port = 59922
$key = "D:\Security\PPS\piratenpartei.ch-ssh-rsa-4096.ppk"

write-host "PiVote build process commencing...";

$bad = .\checkversion.ps1;

if ($bad -eq 0)
{
	$dllpath = (ls "..\client\bin\release\Pirate.PiVote.dll").FullName;
	$version = [Reflection.Assembly]::LoadFile($dllpath).GetName().Version.ToString();

	write-host "Enter password: "
	$pass = read-host

	rm pivote-client.deb
	cd debian
	./build.ps1
	cd ..

	./files.ps1

	./server.ps1
}
else
{
	write-host "Abort";
}