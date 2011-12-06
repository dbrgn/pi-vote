cls

./functions.ps1

$user = "exception"
$server = "fester.piratenpartei.ch"
$port = 22
$dir = "/home/exception/"
$key = "D:\Security\PPS\fester.piratenpartei.ch-ssh-rsa-2048.ppk"
$zip = "C:\Program Files\7-Zip\7z.exe"
$output = ".\output"
$tgt = $user + "@" + $server + ":" + $dir
$svr = $user + "@" + $server

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