for i in *.bmp
do
	convert $i -scale 50% -colors 256 $i.bmp
	mv $i $i.old
	mv $i.bmp $i
done