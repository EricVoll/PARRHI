
#usings
use File::Copy;
use Cwd;
use Cwd 'abs_path';


my $defaultDirectory = getcwd;

my $shouldSkipXSDCreator = shift;

if($shouldSkipXSDCreator != 1){


	my $prog = "bin/Debug/PARS_XSD_Test.exe";

	print "-> Creating XML Scheme (XSD) from XML files";

	if (-f $prog)   # does it exist?
	{
		#my $result1 = `bin/Debug/PARS_XSD_Test.exe`;
		system($prog);
	}
	else  
	{
		print "\n$prog doesn't exist.";
		print "\nexiting";
		exit 0;
	}

}
else{
	print "-> skipped xsd generation";
}

chdir("C:/Program Files (x86)/Microsoft SDKs/Windows/v10.0A/bin/NETFX 4.7 Tools");

print "\n-> Creating C# classes from XML Scheme (XSD)";

my $result = `xsd.exe "$defaultDirectory"/parsScheme.xsd /c /fields /nologo /out:"$defaultDirectory`;

#beautify output if no error happened
if (index($result, "error") != -1) {
   print "\n------Error message:$result\n------";
}
else{

}

if(copy ("$defaultDirectory/parsScheme.xsd",'C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\parrhiScheme.xsd')){

print("\n-> XSD copied to Unity asset folder");
}
else{
	print("\n-> Failed to copy xsd scheme");
}
