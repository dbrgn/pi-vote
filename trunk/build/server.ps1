$serverdir = "..\Server\bin\Release"
$tmp = ".\pivote-server"

rmif $tmp
mkif $tmp
cp -r $serverdir\de-DE $tmp
cp -r $serverdir\fr-FR $tmp
cp $serverdir\Pirate.PiVote.dll $tmp
cp $serverdir\Pirate.PiVote.Server.exe $tmp
cp $serverdir\Emil.GMP.dll $tmp
cp $serverdir\Emil.GMP.dll.config $tmp

plink -i $key -pw $pass -P $port $tgt rm -r pivote-server
pscp -i $key -pw $pass -P $port -r pivote-server $tgt
rmif $tmp