mkif .\tmp
$tmpdll = ".\tmp\" + [System.DateTime]::Now.Ticks.ToString() + ".dll"
cp ..\client\bin\release\Pirate.PiVote.dll $tmpdll
$dllpath = (ls $tmpdll).FullName;
$version = [Reflection.Assembly]::LoadFile($dllpath).GetName().Version.ToString();
$version = $version.SubString(0, $version.Length - 2)
write-host $version;

$pattern1 = "[assembly: AssemblyVersion(""$version"")]";
$pattern2 = "[assembly: AssemblyFileVersion(""$version"")]";
$pattern3 = """ProductVersion"" = ""8:$version""";
$pattern4 = "Version: $version";
$files = (ls -r -filter assemblyinfo.cs ..\);
$bad =  0;

foreach ($file in $files)
{
	$text = [System.IO.File]::ReadAllText($file.FullName);
	if (!$text.Contains($pattern1))
	{
		$message = $file.FullName + " has wrong version.";
		write-host $message;
		$bad++;
	}
}

$file = (ls "..\PiVoteClientSetup\PiVoteClientSetup.vdproj");
$text = [System.IO.File]::ReadAllText($file.FullName);
if (!$text.Contains($pattern3))
{
	$message = $file.FullName + " has wrong version.";
	write-host $message;
	$bad++;
}

$file = (ls ".\debian\package\DEBIAN\control");
$text = [System.IO.File]::ReadAllText($file.FullName);
if (!$text.Contains($pattern4))
{
	$message = $file.FullName + " has wrong version.";
	write-host $message;
	$bad++;
}

return $bad;