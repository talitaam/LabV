import React from "react";

import "./App.scss";
import "bootstrap/dist/css/bootstrap.min.css";

import { Routes } from "./routes/Routes";
import { Layout } from "./shared/components/layout/views/Layout";

function App() {
  return (
    <>
      <Layout>
        <Routes />
      </Layout>
    </>
  );
}

export default App;
