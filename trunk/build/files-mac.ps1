function global:rmif($path)
{
	if ($path -ne $null)
	{
		if (test-path $path)
		{
			rm -r -force $path
		}
	}
}

function global:mkif($path)
{
	if (!(test-path $path))
	{
		New-Item $path -type directory
	}
}

$version = "1.1.4.0"
$zip = "C:\Program Files\7-Zip\7z.exe"
$output = ".\output"

	$tmp = "./pivote-client"
	rmif $tmp
	mkif $tmp
	cp -r ../client/bin/release/de-DE $tmp/
	cp -r ../client/bin/release/fr-FR $tmp/
	cp -r ../circle/bin/release/de-DE $tmp/
	cp -r ../circle/bin/release/fr-FR $tmp/
	cp ../client/bin/release/Emil.GMP.dll $tmp/
	cp ../client/bin/release/MySql.Data.dll $tmp/
	cp ../client/bin/release/Pirate.PiVote.Client.exe $tmp/
	cp ../circle/bin/release/Pirate.PiVote.Circle.exe $tmp/
	cp ../client/bin/release/Pirate.PiVote.Gui.dll $tmp/
	cp ../client/bin/release/Pirate.PiVote.dll $tmp/
	cp -r ./files-mac/* $tmp/
	$name = "PiVote_Client_MaxOsX_x86_" + $version + ".zip"
	pushd
	cd $tmp
	& $zip a -tzip -r $name *
	popd
	mv -force $tmp\$name $output\
	rmif $tmp
