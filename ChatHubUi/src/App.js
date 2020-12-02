import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import Layout from "./components/Layout/Layout";
import Register from "./components/Register/Register";

const App = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/chat" component={Layout} />
        <Route path="/" component={Register} />
      </Switch>
    </BrowserRouter>
  );
};

export default App;
