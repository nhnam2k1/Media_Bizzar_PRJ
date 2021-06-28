using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    public class EmployeeDAL: BaseDAL
    {

        public void Insert(string[] employee_bindings)
        {
            executeNonQuery("INSERT INTO `employee`(`first_name`, `last_name`,  `email`, `address`, `city`, `country`," +
                  " `phone_number`, `wage`, `gender`, `contract_id`,`department_id`, `username`, `password`)" +
                               "VALUES(@first_name,@last_name,@email,@address,@city,@country,@wage,@phone_number,@gender,@contract_id,@department_id,@username,@password)", employee_bindings);
        }
        public void Update(string[] employee_bindings)
        {

            executeNonQuery("UPDATE `employee` SET `first_name`= @firstname,`last_name`= @secondname," +
                 "`address`= @adress,`city`= @city,`country`= @country,`phone_number`=@phonenumber,`gender`=@gender,`email`=@email," +
                 "`department_id`= @departmentid,`contract_id` = @contractid,`wage` = @wage,`username`= @username,`password`= @password WHERE id = @id", employee_bindings);
        }
        public void Delete(string[] employee_bindings)
        {
            executeNonQuery("DELETE FROM `employee` WHERE `id` = @id", employee_bindings);
        }

    }
}                                                                      
