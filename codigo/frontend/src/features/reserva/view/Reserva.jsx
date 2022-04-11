import { Modal } from "../../../shared/components/modal";
import React, { useEffect, useState } from "react";
import api from "../../../api";
import { Table } from "../../../shared/components/table";
import { ManipulacaoReserva } from "./ManipulacaoReserva";

let reservaArray = []

export function Reserva() {
  const [reservas, setReservas] = useState([]);

  useEffect(() => {
    getReservas();
  }, []);

  function getReservas() {
    api.get("/api/v1/reserva").then((res) => {
      let lengthRes = res.data.length
      reservaArray = new Array(lengthRes);

      for (var i = 0; i < reservaArray.length; i++) {
          //var dataRes = new Date(res.data[i].dataReserva);
          //var dataHoje = new Date();
          //if (!(dataHoje > dataRes)) {
              reservaArray[i] = {id: String(res.data[i].id), 
              dataReserva: String(new Date(res.data[i].dataReserva).toLocaleDateString()), 
              turnoReserva: String(res.data[i].turnos.reduce((acc, item) => `${acc}- ${item.descricao}  ` ,'')), 
              horarioChegadaEsperada: String(res.data[i].horarioChegadaEsperada), 
              mesaReserva: String(res.data[i].mesas.reduce((acc, item) => `${acc}- ${item.nomeMesa}   ` ,'')), 
              nomeCliente: String(res.data[i].cliente.nome), 
              statusMesa: String(res.data[i].status.descricao) };
          //};
      }
      setReservas(reservaArray);
    });
  }

  function deleteReserva(id) {
    for (var i = 0; i < reservaArray.length; i++) {
      if ((reservaArray[i].id == id) && (reservaArray[i].statusMesa == "Cancelado")) {
          alert("Reserva já excluída/cancelada.");
          getReservas();
          return;
      }
    }
      var IdReserva = parseInt(id);
      var IdStatus = 4;
      const data = {
        IdReserva,
        IdStatus,
      };
      console.log(data)
      api.put("/api/v1/reserva/status", data).catch((err) => {
        // TODO: tratar erros
      })
      .then(() => {getReservas();});
  }

  const columns = [
    {
      title: "Número",
      path: "id",
    },
    {
      title: "Dia Reserva",
      path: "dataReserva",
    },
    {
      title: "Turno(s)",
      path: "turnoReserva",
    },
    {
      title: "Horário Chegada",
      path: "horarioChegadaEsperada",
    },
    {
      title: "Mesa(s)",
      path: "mesaReserva",
    },
    {
      title: "Cliente",
      path: "nomeCliente",
    },
    {
      title: "Status",
      path: "statusMesa",
    },
  ];

  return (
    <div>
      <h3 className="page-title">Página Inicial de Reservas</h3>
      <Table
        columns={columns}
        docs={reservas}
        remove
        removeAction={(id) => deleteReserva(id)}
        removeConfirmation="Tem certeza que deseja excluir esta reserva?"
        edit
        editPage={(id) => (
          <Modal
            _key={`editStatus${id}`}
            title="Editar Status Reserva"
            button={<i className="fas cursor-pointer theme-icon fa-edit" />}
            content={<ManipulacaoReserva id={id} />}
          />
        )}
        
      />
    </div>
  );
}
