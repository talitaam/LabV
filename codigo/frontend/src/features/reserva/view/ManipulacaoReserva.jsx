import { useEffect, useState } from "react";
import api from "../../../api";

let statusFront = [];
let mesDropDown = "";


export function ManipulacaoReserva({ id }) {
  const [IdStatusA, setIdStatus] = useState(0);
 
 
  useEffect(() => {
      api.get("/api/v1/reserva").then((res) => {
        let lengthRes = res.data.length;
        for (var i = 0; i < lengthRes; i++) {
          if (res.data[i].id == id) {
            if (res.data[i].status.descricao == "Cancelado"){
                console.log("entrou descriçao " + res.data[i].status.descricao)
                statusFront = [];
                mesDropDown = "Status Cancelado";
              }
            else{
              statusFront = [
                {
                  id: "2",
                  descricao: "Em uso",
                },
                {
                  id: "3",
                  descricao: "Finalizado",
                },
              ];
              mesDropDown = "Selecione";
            }
            setIdStatus(res.data[i].status.id|| 0);
            console.log("setIdStatus " + res.data[i].status.id)
            console.log("statusFront " + statusFront)
          }
        }
    });
  }, [id]);


  function updateStatus(e) {
    var IdReserva = parseInt(id);
    var IdStatus = parseInt(IdStatusA);
    const data = {
      IdReserva,
      IdStatus,
    };
    
    console.log(data)

    api.put("/api/v1/reserva/status", data).catch((err) => {
      // TODO: tratar erros
      console.log(err)
    });
  }

  return (
    <div>
      <form>
        <div className="form-group">
          <label htmlFor="nomeMesa">Status da Reserva</label>
          <div>
            <select onChange={(e) => setIdStatus(e.target.value)} required>
            <option value="" selected disabled hidden>{mesDropDown}</option>
              {statusFront.map((option) => (
                <option value={option.id}>{option.descricao}</option>
                ))}
            </select>
          </div>
        </div>
        <div className="d-flex justify-content-end">
          <button
            type="submit"
            onClick={(e) => (updateStatus(e))}
            className="btn btn-primary"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  );
}