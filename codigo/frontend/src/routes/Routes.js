import React from "react";
import { Switch, Route, Redirect } from "react-router";

import { Home } from "../features/home";
import { Turno, DetalhesTurno } from "../features/turno";
import { Mesa, DetalhesMesa } from "../features/mesas";
import { Reserva, DetalhesReserva } from "../features/reserva";

export function Routes() {
  return (
    <Switch>
      <Route exact path="/" component={Home} />
      <Route exact path="/reservas" component={Reserva} />
      <Route exact path="/reservas/detalhes" component={DetalhesReserva} />
      <Route exact path="/turnos" component={Turno} />
      <Route exact path="/turnos/detalhes" component={DetalhesTurno} />
      <Route exact path="/mesas" component={Mesa} />
      <Route exact path="/mesas/detalhes" component={DetalhesMesa} />
      <Redirect from="*" to="/" />
    </Switch>
  );
}
