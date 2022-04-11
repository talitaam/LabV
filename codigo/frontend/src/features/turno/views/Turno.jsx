import { Modal } from "../../../shared/components/modal";
import React, { useEffect, useState } from "react";
import api from "../../../api";
import { Table } from "../../../shared/components/table";
import { ManipulacaoTurno } from "./ManipulacaoTurno";
import { ManipulacaoDiasFuncionamento } from "./ManipulacaoDiasFuncionamento";

export function Turno() {
  const [turnos, setTurnos] = useState([]);

  useEffect(() => {
    getTurnos();
  }, []);

  function getTurnos() {
    api.get("/api/v1/turno").then((res) => {
      setTurnos(res.data);
    });
  }
  function deleteTurno(id) {
    api
      .delete(`/api/v1/turno/${id}`)
      .then(() => {
        getTurnos();
      })
      .catch((err) => {
        // TODO: tratar erros
      });
  }

  const columns = [
    {
      title: "Turno",
      path: "descricao",
    },
    {
      title: "Horário de Início",
      path: "horarioInicio",
    },
    {
      title: "Horário de Fim",
      path: "horarioFim",
    },
  ];

  return (
    <div>
      <h3 className="page-title">Página Inicial de Turno</h3>
      <div className="d-flex justify-content-end mb-3">
        <div className="m-1">
          <Modal
            _key="addTurno"
            title="Adicionar turno"
            button={
              <button type="button" className="btn btn-theme">
                Adicionar turno
              </button>
            }
            content={<ManipulacaoTurno />}
          />
        </div>
        <div className="m-1">
          <Modal
            _key="diasFuncionamento"
            title="Dias de funcionamento"
            button={
              <button type="button" className="btn btn-light">
                Dias de funcionamento
              </button>
            }
            content={<ManipulacaoDiasFuncionamento />}
          />
        </div>
      </div>
      <Table
        columns={columns}
        docs={turnos}
        remove
        removeAction={(id) => deleteTurno(id)}
        removeConfirmation="Tem certeza que deseja excluir esse turno?"
        edit
        editPage={(id) => (
          <Modal
            _key={`editTurno${id}`}
            title="Editar turno"
            button={<i className="fas cursor-pointer theme-icon fa-edit" />}
            content={<ManipulacaoTurno id={id} />}
          />
        )}
      />
    </div>
  );
}
