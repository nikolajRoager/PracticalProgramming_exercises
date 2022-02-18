all: Out.txt
	cat $<

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs genlist.dll
	mcs -target:exe -out:$@ -reference:genlist.dll $<

genlist.dll: genlist.cs
	mcs  -target:library -out:$@ $<

clean:
	rm -f *.dll *.exe [Oo]ut* [Ll]og*
