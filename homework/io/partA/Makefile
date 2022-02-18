Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I saved the actual output as html, so try opening them in your browser of choice
#Log is incidentally created at the same time as out
all : outa.html
;cat loga.txt
;outa.html


outa.html : partA.exe input.txt
;cat input.txt | $(Runtime) $< 1> $@ 2>loga.txt

partA.exe : partA.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* [Ll]og* *.exe
# remove secondary files
