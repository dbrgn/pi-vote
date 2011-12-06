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