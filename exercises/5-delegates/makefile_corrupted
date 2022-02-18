all: Out.txt
	cat $<

Out.txt: genlist.exe
	mono genlist.exe > Out.txt

genlist.exe: main.cs genlist.dll
	mcs -target:exe -out:$@ -reference:genlist.dll $<

genlist.dll: genlist.cs
	mcs  -target:library -out:$@ $<

clean:
	rm -f *.dll *.exe [Oo]ut* [Ll]og*
