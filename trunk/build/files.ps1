	rm $output\*.deb
	rm $output\*.zip
	mkif $output\

	$name = "PiVote_Client_Debian_x86_" + $version + ".deb"
	cp debian/pivote-client.deb $name
	mv -force $name $output\
	
	$tmp = "./pivote-client"
	rmif $tmp
	mkif $tmp
	cp -r ../client/bin/release/de-DE $tmp/
	cp -r ../client/bin/release/fr-FR $tmp/
	cp ../client/bin/release/Emil.GMP.dll $tmp/
	cp ../client/bin/release/MySql.Data.dll $tmp/
	cp ../client/bin/release/Pirate.PiVote.Client.exe $tmp/
	cp ../client/bin/release/Pirate.PiVote.Gui.dll $tmp/
	cp ../client/bin/release/Pirate.PiVote.dll $tmp/
	cp -r ./files-linux/* $tmp/
	$name = "PiVote_Client_Linux_x86_" + $version + ".zip"
	pushd
	cd $tmp
	& $zip a -tzip -r $name *
	popd
	mv -force $tmp\$name $output\
	rmif $tmp
	
	$tmp = "./pivote-client"
	rmif $tmp
	mkif $tmp
	cp -r ../client/bin/release/de-DE $tmp/
	cp -r ../client/bin/release/fr-FR $tmp/
	cp ../client/bin/release/Emil.GMP.dll $tmp/
	cp ../client/bin/release/MySql.Data.dll $tmp/
	cp ../client/bin/release/Pirate.PiVote.Client.exe $tmp/
	cp ../client/bin/release/Pirate.PiVote.Gui.dll $tmp/
	cp ../client/bin/release/Pirate.PiVote.dll $tmp/
	cp -r ./files-windows/* $tmp/
	cp -r ./files-linux/* $tmp/
	mkdir $tmp/Data
	$name = "PiVote_Client_Portable_x86_" + $version + ".zip"
	pushd
	cd $tmp
	& $zip a -tzip -r $name *
	popd
	mv -force $tmp\$name $output\
	rmif $tmp

	$tmp = "./admin-package"
	rmif $tmp
	mkif $tmp
	cp -r ../cagui/bin/release/de-DE $tmp/
	cp -r ../cagui/bin/release/fr-FR $tmp/
	cp ../cagui/bin/release/Emil.GMP.dll $tmp/
	cp ../cagui/bin/release/MySql.Data.dll $tmp/
	cp ../cagui/bin/release/CaGui.exe $tmp/
	cp ../cagui/bin/release/Pirate.PiVote.Gui.dll $tmp/
	cp ../cagui/bin/release/Pirate.PiVote.dll $tmp/
	cp -r ./files-windows/* $tmp/
	cp -r ./files-linux/* $tmp/
	$name = "PiVote_Admin_Package_x86_" + $version + ".zip"
	pushd
	cd $tmp
	& $zip a -tzip -r $name *
	popd
	mv -force $tmp\$name $output\
	rmif $tmp

	$name = "PiVote_Client_Windows_x86_" + $version + ".zip"
	& $zip a -tzip $name ..\PiVoteClientSetup\Release\PiVoteClientSetup.msi
	mv -force $name $output\
