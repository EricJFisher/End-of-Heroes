//"End of Heroes" is a rougelite role playing game where players play
//as a 'monster' fighting back against the 'heroes' that have been wiping
//out 'monsters'.
//Copyright (C) 2020 Eric James Fisher

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://www.gnu.org/licenses/>.

using EndOfHeroes.Services;
using System;

namespace EndOfHeroes
{

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            CrashHandler.Bind();

            try
            {
                using (var game = new Desktop())
                    game.Run();
            }
            catch (Exception e)
            {
                CrashHandler.Report(e);
            }
        }
    }
}
