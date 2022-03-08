class main{
static void Main(){
	var ma=new matrix("1 2 ; 5 6");
	ma.print();
	var ma_T=  ma.T;
    var maaT=ma*ma_T;

    maaT.print();
}
}
