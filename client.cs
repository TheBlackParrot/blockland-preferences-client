$Pref::BLPrefs::ClientDebug = true;

exec("./gui.cs");
exec("./overrides.cs");

package BLPrefClientPackage {
	function GameConnection::setConnectArgs(%a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %n, %o,%p) {
		Parent::setConnectArgs(%a, %b, %c, %d, %e, %f, $BLPrefs::Version, %h, %i, %j, %k, %l, %m, %n, %o, %p);
	}
};
activatePackage(BLPrefClientPackage);