import PropTypes from "prop-types";
import { Header } from "../header/views/Header";
import { Sidebar } from "../sidebar/views/Sidebar";

export function Layout({ children }) {
  return (
    <div className="layout container-fluid">
      <div className="row">
        <nav className="navbar fixed-top navbar-light bg-light">
          <Header />
        </nav>
      </div>
      <div className="row container-content">
        <div className="col-2 px-0 position-fixed sidebar" id="sticky-sidebar">
          <div className="nav flex-column flex-nowrap vh-100 overflow-auto text-white">
            <Sidebar />
          </div>
        </div>
        <div className="col offset-2" id="main">
          <div className="content mt-3">{children}</div>
        </div>
      </div>
    </div>
  );
}

Layout.propTypes = {
  children: PropTypes.node,
};
