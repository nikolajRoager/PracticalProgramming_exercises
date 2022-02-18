Compiler = mcs
Runtime = mono
CSflags =-optimize+
OUT = out
LOG = log

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I saved the actual output as html, so try opening them in your browser of choice
#Log is incidentally created at the same time as out
all : $(OUT)/outa.html  $(OUT)/outb.html $(OUT)/outc.html
;cat $(LOG)/loga.txt
;cat $(OUT)/outa.html
;cat $(LOG)/logb.txt
;cat $(OUT)/outb.html
;cat $(LOG)/logc.txt
;cat $(OUT)/outc.html


$(OUT)/outc.html : partC.exe input.txt
;$(Runtime) $< input.txt output.txt  >$(LOG)/logc.txt

partC.exe : partC.cs Makefile
;$(Compiler) $< -target:exe -out:$@

$(OUT)/outb.html : partB.exe input.txt
;$(Runtime) $< $$(cat input.txt) 1> $@ 2>$(LOG)/logb.txt

partB.exe : partB.cs Makefile
;$(Compiler) $< -target:exe -out:$@

$(OUT)/outa.html : partA.exe input.txt
;cat input.txt | $(Runtime) $< 1> $@ 2>$(LOG)/loga.txt

partA.exe : partA.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f $(OUT)/[Oo]ut* $(LOG)/[Ll]og* *.exe
# remove secondary files
