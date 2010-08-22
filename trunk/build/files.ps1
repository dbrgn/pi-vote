	rm $output\*.deb
	rm $output\*.zip
	mkif $output\

	$name = "PiVote_Client_Debian_x86_" + $version + ".deb"
	cp debian/pivote-client.deb $name
	mv -force $name $output\
	
	$tmp = "./pivote-client"
	rmif $tmp
	mkif $tmp
	cp -r ../client/bin/release/* $tmp/
	rmif $tmp/*vshost*
	cp -r ./files-linux/* $tmp/
	$name = "PiVote_Client_Linux_x86_" + $version + ".zip"
	pushd
	cd $tmp
	& $zip a -tzip -r $name *
	popd
	mv -force $tmp\$name $output\
	rmif $tmp

	$name = "PiVote_Client_Windows_x86_" + $version + ".zip"
	& $zip a -tzip $name ..\PiVoteClientSetup\Release\PiVoteClientSetup.msi
	mv -force $name $output\
