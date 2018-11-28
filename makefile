
vm.exe : 
	csc vm/*.cs -t:exe -out:vm.exe

test : parseATest parseBTest

parseATest : parseATests.exe
	mono parseATests.exe

parseBTest : parseBTests.exe
	mono parseBTests.exe

parseATests.exe :
	csc parseA/data.cs parseA/lex.cs parseA/test.cs -t:exe -out:parseATests.exe

parseBTests.exe :
	csc parseB/data.cs parseB/lex.cs parseB/parser.cs parseB/test.cs -t:exe -out:parseBTests.exe

clean : 
	rm -rf *.exe
