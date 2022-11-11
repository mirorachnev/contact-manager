import { Contact } from "./contact";

export type ActionModelDialogProps = {  
  onHide: () => void,
  show: boolean,
  edit: boolean,
  reloadData: () => void,
  contact?: Contact
}