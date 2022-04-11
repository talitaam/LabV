import { Link } from "react-router-dom";

export function Header() {
  return (
    <div className="header">
      <div className="container-fluid mt-1">
        <Link to="/">
          <img
            alt=""
            src="/assets/img/icon_svg.svg"
            width="45"
            className="d-inline-block align-top"
          />
        </Link>
        <h3 className="title-header ml-3">Reserve Aqui</h3>
      </div>
    </div>
  );
}
