$dllpath = (ls "..\client\bin\release\Pirate.PiVote.dll").FullName;
$version = [Reflection.Assembly]::LoadFile($dllpath).GetName().Version.ToString();
write-host $version;

$pattern1 = "[assembly: AssemblyVersion(""$version"")]";
$pattern2 = "[assembly: AssemblyFileVersion(""$version"")]";
$pattern3 = """ProductVersion"" = ""8:$version""";
$files = (ls -r -filter assemblyinfo.cs ..\);
$bad =  0;

foreach ($file in $files)
{
	$text = [System.IO.File]::ReadAllText($file.FullName);
	if (!$text.Contains($pattern1) -or !$text.Contains($pattern2))
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


return $bad;