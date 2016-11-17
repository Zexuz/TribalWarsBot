namespace TribalWarsBot.Helpers {

    public static class Constants {

        public static string BaseUrl => "https://sv36.tribalwars.se/game.php?";

        public static string UpgradeBuildingUrl
            => "village=__village__&screen=main&ajaxaction=upgrade_building&type=__type__&h=__csrfToken__";

        public static string CancelOrderUrl
            => "village=__village__&screen=main&ajaxaction=cancel_order&type=__type__&h=__csrfToken__";


        public static int ProdValue => 3600;

        public static class Units {

            public static class Axe {

                public static int Wood => 60;
                public static int Clay => 30;
                public static int Iron => 40;
                public static int FramSpace => 1;
                public static int SpeedInMs => 60 * 1000 * 17;

            }

            public static class Catapult {

                public static int Wood => 320;
                public static int Clay => 400;
                public static int Iron => 100;
                public static int FarmSpace => 8;
                public static int SpeedInMs => 60 * 1000 * 28;

            }

            public static class Heavy {

                public static int Wood => 200;
                public static int Clay => 150;
                public static int Iron => 600;
                public static int FarmSpace => 6;
                public static int SpeedInMs => 60 * 1000 * 10;

            }

            public static class Light {

                public static int Wood => 125;
                public static int Clay => 100;
                public static int Iron => 250;
                public static int FarmSpace => 4;
                public static int SpeedInMs => 60 * 1000 * 9;

            }


            public static class Ram {

                public static int Wood => 300;
                public static int Clay => 200;
                public static int Iron => 200;
                public static int FarmSpace => 5;
                public static int SpeedInMs => 60 * 1000 * 28;

            }

            public static class Spear {

                public static int Wood => 50;
                public static int Clay => 30;
                public static int Iron => 30;
                public static int FarmSpace => 1;
                public static int SpeedInMs => 60 * 1000 * 17;

            }

            public static class Spy {

                public static int Wood => 50;
                public static int Clay => 50;
                public static int Iron => 20;
                public static int FarmSpace => 2;
                public static int SpeedInMs => 60 * 1000 * 8;

            }

            public static class Sword {

                public static int Wood => 30;
                public static int Clay => 30;
                public static int Iron => 70;
                public static int FarmSpace => 1;
                public static int SpeedInMs => 60 * 1000 * 20;

            }

            public static class Knight {

                public static int Wood => 20;
                public static int Clay => 20;
                public static int Iron => 40;
                public static int FarmSpace => 10;
                public static int SpeedInMs => 60 * 1000 * 9;

            }

            public static class Snob {

                public static int Wood => 40000;
                public static int Clay => 50000;
                public static int Iron => 50000;
                public static int FarmSpace => 100;
                public static int SpeedInMs => 60 * 1000 * 32;

            }

        }

    }

}