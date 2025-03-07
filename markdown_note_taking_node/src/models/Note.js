import { Model } from "sequelize";
import db from "../db/config.js";

const Note = db.define('Notes', {
    id: {
        type: db.Sequelize.INTEGER,
        primaryKey: true,
        autoIncrement: true
    },
    title: {
        type: db.Sequelize.STRING,
        allowNull: false
    },
    content: {
        type: db.Sequelize.TEXT,
        allowNull: false
    },
    contentParsedToHtml: {
        type: db.Sequelize.TEXT,
        allowNull: true
    }
})

Note.addHook('beforeCreate', (note, options) => {
    note.title = note.title ?? `Note${note.id}_${new Date().toISOString()}`
})

export default Note