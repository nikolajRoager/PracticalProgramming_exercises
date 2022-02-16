Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I want make to update if i change the makefie, so I make this dependent on the makefile
all : log.txt Out-stdin.txt Out.txt Makefile cmdline.txt
;cat Out-stdin.txt
;cat log.txt
;cat Out.txt
;cat cmdline.txt

cmdline.txt : cmdline.exe
;$(Runtime) $< 423.32 20.33 bannana 10.2 1> $@ 2>logCmd.txt

Out.txt : fileio.exe
;$(Runtime) $< 1> $@ 2>logIo.txt

fileio.exe : fileio.cs Makefile
;$(Compiler) $< -target:exe -out:$@

cmdline.exe : cmdline.cs Makefile
;$(Compiler) $< -target:exe -out:$@

Out-stdin.txt : stdin.exe In.txt Makefile
;cat In.txt | $(Runtime) $< 1> $@ 2>logStd.txt

stdin.exe : stdin.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* *.exe
# remove secondary files
