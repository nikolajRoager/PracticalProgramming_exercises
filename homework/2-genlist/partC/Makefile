Compiler = mcs
Runtime = mono
CSflags =-optimize+

.RECIPEPREFIX := ;

all: Out.txt
;cat $<

Out.txt: main.exe data.txt
;$(Runtime) $< data.txt >$@

main.exe: main.cs linkedList.dll
;$(Compiler) $< -reference:linkedList.dll -target:exe -out:$@

linkedList.dll: linkedList.cs node.cs
;$(Compiler) $^ -target:library -out:$@

clean:
;rm -f *.dll *.exe [Oo]ut*
