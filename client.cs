$Pref::BLPrefs::ClientDebug = true;

exec("./gui.cs");
exec("./overrides.cs");
exec("./rows.cs");

package BLPrefClientPackage {
	function GameConnection::setConnectArgs(%a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %n, %o,%p) {
		Parent::setConnectArgs(%a, %b, %c, %d, %e, %f, %g, $BLPrefs::Version, %i, %j, %k, %l, %m, %n, %o, %p);
	}
};
activatePackage(BLPrefClientPackage);