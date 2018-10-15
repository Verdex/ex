
vm.exe : 
	csc vm/*.cs -t:exe -out:vm.exe


clean : 
	rm -rf *.exe
