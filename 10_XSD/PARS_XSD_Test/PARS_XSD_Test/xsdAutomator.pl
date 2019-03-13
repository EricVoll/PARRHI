
#usings
use File::Copy;
use Cwd;
use Cwd 'abs_path';


my $defaultDirectory = getcwd;

my $result1 = `PARS_XSD_Test.exe`;

my $prog = "PARS_XSD_Test.exe";

print "-> Creating XML Scheme (XSD) from XML files";

if (-f $prog)   # does it exist?
{
	system($prog);
}
else  
{
	print "\n$prog doesn't exist.";
	print "\nexiting";
	exit 0;
}

chdir("../../../../../../../../Program Files (x86)/Microsoft SDKs/Windows/v10.0A/bin/NETFX 4.7 Tools");

print "\n-> Creating C# classes from XML Scheme (XSD)";

my $result = `xsd.exe "$defaultDirectory"/parsScheme.xsd /c /out:"$defaultDirectory`;
#my $result = `./xsd.exe sign /f "$PfxFilePath" /p $PfxPassword /t $TimeStampServerUrl "$FileToSign"`;

#beautify output if no error happened
if (index($result, "error") != -1) {
   print "\n------Error message:$result\n------";
}
else{

}
