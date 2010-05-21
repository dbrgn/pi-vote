rm -r pivote-server
mkdir pivote-server
cp -r ..\Server\bin\Release\de-DE .\pivote-server\
cp -r ..\Server\bin\Release\fr-FR .\pivote-server\
cp ..\Server\bin\Release\Pirate.PiVote.dll .\pivote-server\
cp ..\Server\bin\Release\Pirate.PiVote.Server.exe .\pivote-server\
cp ..\Server\bin\Release\Emil.GMP.dll .\pivote-server\
cp ..\Server\bin\Release\Emil.GMP.dll.config .\pivote-server\

plink -i $key -pw $pass -P $port $tgt rm -r pivote-server
pscp -i $key -pw $pass -P $port -r pivote-server $tgt