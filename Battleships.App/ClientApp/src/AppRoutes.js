import { Home } from "./components/views/Home";
import { Game } from "./components/views/Game";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/game',
        element: <Game />
    }
];

export default AppRoutes;
