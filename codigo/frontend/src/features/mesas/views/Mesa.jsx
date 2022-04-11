import { Modal } from "../../../shared/components/modal";
import React, { useEffect, useState } from "react";
import api from "../../../api";
import { Table } from "../../../shared/components/table";
import { ManipulacaoMesa } from "./ManipulacaoMesa";


export function Mesa() {
  const [mesas, setMesas] = useState([]);

  useEffect(() => {
    getMesas();
  }, []);

  function getMesas() {
    api.get("/api/v1/mesa").then((res) => { //mudar depois
      setMesas(res.data);
    });
  }
  function deleteMesa(id) {
    api
      .delete(`/api/v1/mesa/${id}`) //mudar depois
      .then(() => {
        getMesas();
      })
      .catch((err) => {
        // TODO: tratar erros
      });
  }

  const columns = [
    {
      title: "Nome Mesa",
      path: "nomeMesa",
    },
    {
      title: "Quantidade de Lugares",
      path: "quantCadeiras",
    },
  ];

  return (
    <div>
      <h3 className="page-title">Página Inicial de Mesas</h3>
      <div className="d-flex justify-content-end mb-3">
        <div className="m-1">
          <Modal
            _key="addMesa"
            title="Adicionar mesa"
            button={
              <button type="button" className="btn btn-theme">
                Adicionar mesa
              </button>
            }
            content={<ManipulacaoMesa />}
          />
        </div>
      </div>
      <Table
        columns={columns}
        docs={mesas}
        remove
        removeAction={(id) => deleteMesa(id)}
        removeConfirmation="Tem certeza que deseja excluir essa mesa?"
        edit
        editPage={(id) => (
          <Modal
            _key={`editMesa${id}`}
            title="Editar mesa"
            button={<i className="fas cursor-pointer theme-icon fa-edit" />}
            content={<ManipulacaoMesa id={id} />}
          />
        )}
      />
    </div>
  );
}


