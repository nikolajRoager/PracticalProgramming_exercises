Compiler = mcs
Runtime = mono
CSflags =-optimize+

.RECIPEPREFIX := ;

all: Out.txt
;cat $<

Out.txt: main.exe data.txt
;$(Runtime) $< data.txt >$@

main.exe: main.cs genlist.dll
;$(Compiler) $< -reference:genlist.dll -target:exe -out:$@

genlist.dll: genlist.cs
;$(Compiler) $< -target:library -out:$@

clean:
;rm -f *.dll *.exe [Oo]ut*
