import PropTypes from "prop-types";
import React from "react";
import { ConfirmationDialog } from "../../dialog/index";

export function Table({ columns, docs, ...props }) {
  return (
    <table className="table table-sm table-striped">
      <thead>
        <tr>
          {columns.map((column, i) => (
            <th key={i} scope="col">
              {column.title}
            </th>
          ))}

          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <Rows />
      </tbody>
    </table>
  );

  function Rows() {
    return docs.map((doc, i) => {
      return (
        <tr key={i}>
          {columns.map((column, i) => {
            let row = doc[column.path];
            return <td key={i}>{row}</td>;
          })}
          {props.remove && (
            <td className="d-flex">
              {renderEditPage(doc.id)}
              <Remove id={doc.id} />
            </td>
          )}
        </tr>
      );
    });
  }

  function renderEditPage(id) {
    return props.editPage(id);
  }

  function Remove({ id }) {
    return (
      <ConfirmationDialog
        description={props.removeConfirmation}
        icon={<i className="fas fa-trash cursor-pointer theme-icon ml-1" />}
        nameProceedButton="Excluir"
        toProceed={() => props.removeAction(id)}
      ></ConfirmationDialog>
    );
  }
}

Table.propTypes = {
  docs: PropTypes.array.isRequired,
  columns: PropTypes.array.isRequired,
};
