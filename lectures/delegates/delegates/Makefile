CSC = mcs
RUN = mono


all:Out.txt
	cat $<
Out.txt: main.exe
	$(RUN) main.exe > Out.txt
main.exe: main.cs
	$(CSC) -target:exe -out:$@ $<

clean:
	$(RM) *.dll *.exe [Oo]ut* [Ll]og*
