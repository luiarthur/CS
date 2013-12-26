out <- read.table("draws.txt",header=F)[,1]
curve(dnorm(x,3,2),from=-8,to=12,col='red',lwd=3)
lines(density(out),col='blue',lwd=3,lty=3)
legend("topright",legend=c("True","Bayes"),col=c("red","blue"),lwd=3)
