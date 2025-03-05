export class Todo{
    id:string;
    description:string;
    title:string;
    userId:string;
    backgroundColor:number;
    isCompleted:boolean;

    createdDate:Date;
    updatedDate?:Date;
    deletedDate?:Date;
}