
vm.exe : 
	csc vm/*.cs -t:exe -out:vm.exe

test : parseCTest

parseATest : parseATests.exe
	mono parseATests.exe

parseBTest : parseBTests.exe
	mono parseBTests.exe

parseCTest : parseCTests.exe
	mono parseCTests.exe

parseATests.exe :
	csc parseA/data.cs parseA/lex.cs parseA/test.cs -t:exe -out:parseATests.exe

parseBTests.exe :
	csc parseB/data.cs parseB/lex.cs parseB/parser.cs parseB/test.cs -t:exe -out:parseBTests.exe

parseCTests.exe :
	fsharpc parseC/Data.fs parseC/Util.fs parseC/Lex.fs parseC/Parser.fs parseC/Test.fs --out:parseCTests.exe

clean : 
	rm -rf *.exe
	rm -rf *.dll
