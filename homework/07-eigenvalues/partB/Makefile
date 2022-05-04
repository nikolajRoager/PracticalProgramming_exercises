
DATADIR = DATA

CSC = mcs
RUN = mono

all : logB energies.png
	cat logB

#A single demonstration, just to see that it works
$(DATADIR)/wavefunction_80_500.tsv logB: B_hydrogen.exe
	echo 'Demonstration width range 80 a and N=500' > logB
	echo "R_max   N     E[0]     E[1]   E[2]   E[3]   E[4]" >> logB
	$(RUN) $< 80 500 2>> logB 1> $(DATADIR)/wavefunction_80_500.tsv

$(DATADIR)/wavefunction_analytic.tsv: analytical_wavefunctions.exe
	$(RUN) $< 80 500 2>> logB 1> $(DATADIR)/wavefunction_analytical.tsv


energies.png : $(DATADIR)/energies.tsv plot.gpi $(DATADIR)/wavefunction_80_500.tsv $(DATADIR)/wavefunction_analytic.tsv
	pyxplot plot.gpi

$(DATADIR)/energies.tsv: B_hydrogen.exe
	$(RUN) $< 50 10 2> $(DATADIR)/energies.tsv 1> /dev/null ;\
	number=10 ; while [[ $$number -le 250 ]] ; do \
		echo Running hydrogren numeric diagonalization with r_max=50 a and resolution $$number, out of 250  ; \
        $(RUN) $< 50 $$number 2>> $(DATADIR)/energies.tsv 1>  /dev/null ;\
		((number = number + 10)) ; \
	done
	$(RUN) $< 10 40 2> $(DATADIR)/energiesR.tsv 1>  /dev/null ;\
	R=15 ; N=60 ; while [[ $$R -le 50 ]] ; do \
		echo Running hydrogren numeric diagonalization with r_max=$$R out of 80 a and resolution $$N -- so N/r_max=4; \
        $(RUN) $< $$R $$N 2>> $(DATADIR)/energiesR.tsv 1> /dev/null ;\
		((R = R + 5)) ; \
		((N = N + 20)) ; \
	done

analytical_wavefunctions.exe: analytical_wavefunctions.cs
	$(CSC) -target:exe -out:$@ $<

B_hydrogen.exe:main.cs jacobi.dll matrix.dll
	$(CSC) -target:exe -out:$@  -reference:jacobi.dll   -reference:matrix.dll $<

matrix.dll : matrix.cs
	$(CSC) $< -target:library  -out:$@

fit.dll : fit.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@


jacobi.dll : jacobi.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@

.PHONY: clean

clean:
	rm -f *.dll *.exe log* *.png fit*.tsv $(DATADIR)/*.tsv
