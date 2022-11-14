import { Notify } from "quasar";

const showSucsessNotify = (message) =>
{
    Notify.create({
        color: "green-4",
        textColor: "white",
        message: message,
      });
}

const showErrorNotify = (message) =>
{
    Notify.create({
        color: "red-5",
        textColor: "white",
        message: message,
      });
}

const showWarningNotify = (message) =>
{
    Notify.create({
        color: "yellow-4",
        textColor: "black",
        message: message,
      });
}

export default { showSucsessNotify, showErrorNotify, showWarningNotify }