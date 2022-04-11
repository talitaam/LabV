import { Link } from "react-router-dom";

export function Menu() {
  return (
    <div className="menu">
      <ul className="nav flex-column p2">
        <span className="text-center mt-4 mb-4">MENU</span>
        <li className="">
          <i className="fas fa-utensils" />
          <Link to="/reservas">Reservas</Link>
        </li>
        <li className="">
          <i className="fas fa-clock" />
          <Link to="/turnos">Cadastro de Turnos</Link>
        </li>
        <li className="">
          <i className="fas fa-table" />
          <Link to="/mesas">Cadastro de Mesas</Link>
        </li>
      </ul>
    </div>
  );
}
