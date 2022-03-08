DATA = out.data
PLOT = plot.svg
all: $(PLOT)
$(DATA): main.exe
	mono $< 1> $@ 2> Log
	
main.exe: main.cs matlib.dll
	mcs $< -reference:matlib.dll

matlib.dll: ode.cs ../matrix/vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.exe out* *.svg [Ll]og*

$(PLOT): $(DATA) Makefile
	echo '\
	set term svg background rgb "white";\
	set out "$(PLOT)";\
	set key out;\
	set tics out;\
	set xlabel "x";\
	plot \
	 "$(DATA)" using 1:2 with linespoints pt 7 ps 0.5 title "sin" \
	,"$(DATA)" using 1:3 with linespoints pt 5 ps 0.5 title "cos" \
	,"$(DATA)" using ($$1):(0) w p pt 7 pointsize 0.1 not \
	'|gnuplot
	
