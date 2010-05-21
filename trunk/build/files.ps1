	rm *.deb
	rm *.zip

	$name = "PiVote_Client_Debian_x86_" + $version + ".deb"
	cp debian/pivote-client.deb $name
	
	rm -r ./pivote-client
	mkdir ./pivote-client
	cp -r ../client/bin/release/* ./pivote-client/
	rm ./pivote-client/*vshost*
	$name = "PiVote_Client_Linux_x86_" + $version + ".zip"
	cd pivote-client
	& 'C:\Program Files (x86)\7-Zip\7z.exe' a -tzip -r $name *
	cd ..
	mv .\pivote-client\*.zip .

	$name = "PiVote_Client_Windows_x86_" + $version + ".zip"
	& 'C:\Program Files (x86)\7-Zip\7z.exe' a -tzip $name ..\PiVoteClientSetup\Release\PiVoteClientSetup.msi
