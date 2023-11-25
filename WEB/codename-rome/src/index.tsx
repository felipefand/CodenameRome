import { createRoot } from "react-dom/client";
import {
  createBrowserRouter,
  RouterProvider,
  Route,
  Link,
} from "react-router-dom";
import HomePage from "./routes/HomePage";
import MenuPage from "./routes/MenuPage";
import SettingsPage from "./routes/SettingsPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage/>
  },
  {
    path: "menu",
    element: <MenuPage/>,
  },
  {
    path: "settings",
    element: <SettingsPage/>,
  },
]);

createRoot(document.getElementById("root") as HTMLElement).render(
  <RouterProvider router={router} />
);