cls

./functions.ps1

$user = "exception"
$server = "lechuck.piratenpartei.ch"
$port = 59922
$dir = "/home/exception/"
$key = "D:\Security\PPS\piratenpartei.ch-ssh-rsa-4096.ppk"
$zip = "C:\Program Files (x86)\7-Zip\7z.exe"
$output = ".\output"
$tgt = $user + "@" + $server + ":" + $dir

write-host "PiVote build process commencing...";

$bad = .\checkversion.ps1;

if ($bad -eq 0)
{
	$dllpath = (ls "..\client\bin\release\Pirate.PiVote.dll").FullName;
	$version = [Reflection.Assembly]::LoadFile($dllpath).GetName().Version.ToString();

	write-host "Enter password: "
	$pass = read-host

	rmif pivote-client.deb
	pushd
	cd debian
	./build.ps1
	popd

	./files.ps1

	./server.ps1
}
else
{
	write-host "Abort";
}