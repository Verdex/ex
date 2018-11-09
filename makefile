
vm.exe : 
	csc vm/*.cs -t:exe -out:vm.exe

test : parseATest

parseATest : parseATests.exe
	mono parseATests.exe

parseATests.exe :
	csc parseA/data.cs parseA/lex.cs parseA/test.cs -t:exe -out:parseATests.exe

clean : 
	rm -rf *.exe
