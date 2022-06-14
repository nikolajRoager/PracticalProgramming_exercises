.RECIPEPREFIX := ;

RUN = python3

all: OlympusMons_data.tsv

OlympusMons_data.tsv : make_rectgrid.py
;$(RUN) $< OlympusMons.tiff 8 8 40 65 53 78 1> $@

.PHONY: clean

clean:
;rm -f *.tsv
